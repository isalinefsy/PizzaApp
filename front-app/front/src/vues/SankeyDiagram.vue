<template>
    <FSRow id="Sankey" width="fill" height="fill" align="top-center" padding="20px">
        <FSSpan font="text-h2" :label="`Diagramme de Sankey`"/>
        <div ref="chart" class="chart-container"></div>
    </FSRow>
</template>
<script lang="ts">
  import { defineComponent, ref, onMounted, nextTick } from "vue";
  import axios from 'axios';
  import * as echarts from "echarts";
  
  import FSCol from "@dative-gpi/foundation-shared-components/components/FSCol.vue";
  import FSRow from "@dative-gpi/foundation-shared-components/components/FSRow.vue";
  import FSSpan from "@dative-gpi/foundation-shared-components/components/FSSpan.vue";
  
  export default defineComponent({
    components: {
      FSCol,
      FSRow,
      FSSpan,
    },
    setup() {
      const chart = ref<HTMLDivElement | null>(null);
  
      const initChart = async () => {
        await nextTick();
        if (chart.value) {
          const myChart = echarts.init(chart.value);
          try {
            const response = await axios.get('./data/example.json');
            const data = response.data;
  
            const option: echarts.EChartsOption = {
              title: {
                text: 'Sankey Diagram'
              },
              tooltip: {
                trigger: 'item',
                triggerOn: 'mousemove'
              },
              series: [
                {
                  type: 'sankey',
                  data: data.nodes,
                  links: data.links,
                  emphasis: {
                    focus: 'adjacency'
                  },
                  lineStyle: {
                    color: 'gradient',
                    curveness: 0.5
                  }
                }
              ]
            };
            myChart.setOption(option);
          } catch (error) {
            console.error('Error fetching Sankey data:', error);
          }
        }
      };
  
      onMounted(initChart);
  
      return {
        chart,
      };
    },
  });
  </script>
<style>
.chart-container {
  width: 100%;
  height: 500px;
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 60px;
}
</style>
