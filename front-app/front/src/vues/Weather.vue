<template>
  <FSCol id="weather" width="fill" height="fill" align="top-center" padding="20px">
    <FSRow width="fill" height="hug" align="center-left">
      <FSSpan font="text-h2" :label="'Données météo :'" />
    </FSRow>
    <div ref="chart" class="chart-container"></div>
    <FSRow width="fill" height="hug" align="center-left" padding="8px" gap="16px">
      <FSCol align="center-center" width="fill">
        <FSSpan v-if="selectedTemperature !== null" font="text-h2" :label="`Température sélectionnée : ${selectedTemperature} °C`" />
        <FSSpan v-else font="text-h2" :label="''" />
      </FSCol>
    </FSRow>
  </FSCol>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, nextTick } from "vue";
import WeatherForecastApi, {
  WeatherForecast,
} from "../services/WeatherForecastApiService";
import FSCol from "@dative-gpi/foundation-shared-components/components/FSCol.vue";
import FSRow from "@dative-gpi/foundation-shared-components/components/FSRow.vue";
import FSSpan from "@dative-gpi/foundation-shared-components/components/FSSpan.vue";
import FSButton from "@dative-gpi/foundation-shared-components/components/FSButton.vue";
import FSForm from "@dative-gpi/foundation-shared-components/components/FSForm.vue";
import FSCheckbox from "@dative-gpi/foundation-shared-components/components/FSCheckbox.vue";
import FSIcon from "@dative-gpi/foundation-shared-components/components/FSIcon.vue";
import FSTextField from "@dative-gpi/foundation-shared-components/components/fields/FSTextField.vue";
import FSDivider from "@dative-gpi/foundation-shared-components/components/FSDivider.vue";
import * as echarts from "echarts";

export default defineComponent({
  components: {
    FSCol,
    FSRow,
    FSSpan,
    FSButton,
    FSForm,
    FSCheckbox,
    FSIcon,
    FSTextField,
    FSDivider,
  },
  name: "Weather",
  data() {
    return {
      weathers: [] as WeatherForecast[],
      selectedTemperature: null as number | null,
      active: false,
    };
  },
  async created() {
    await this.fetchWeathers();
  },
  methods: {
    async fetchWeathers() {
      try {
        const data = await WeatherForecastApi.getAll();
        this.weathers = data;
        await nextTick(); // Wait for DOM to update
        this.initChart();
      } catch (error) {
        console.error(
          "Erreur lors de la récupération des données météo:",
          error
        );
      }
    },
    initChart() {
      const chartDom = this.$refs.chart as HTMLElement;
      const myChart = echarts.init(chartDom);
      const dates = this.weathers.map((weather) => weather.date);
      const temperatures = this.weathers.map((weather) => weather.temperatureC);

      const option: echarts.EChartsOption = {
        tooltip: {},
        title: {
          //text: "Latest temperatures",
        },
        xAxis: {
          type: "category",
          data: dates,
        },
        yAxis: {
          type: "value",
        },
        visualMap: {
        top: 50,
        right: 10,
        pieces: [
          {
            gt: 0,
            lte: 30,
            color: 'green'
          },
          {
            gt: 30,
            lte: 50,
            color: 'orange'
          },
          {
            gt: -30,
            lte: 0,
            color: 'blue'
          },
        ],
        outOfRange: {
          color: 'red'
        }
      },
        series: [
          {
            data: temperatures,
            type: "scatter",
          },
        ],
      };

      myChart.setOption(option);

      // Add click event listener
      myChart.on("mouseover", (params) => {
        if (params.componentType === "series") {
          this.selectedTemperature = params.data as number;
        }
      });

      myChart.on("click", (params) => {
        if (params.componentType === "series") {
          this.selectedTemperature = params.data as number;
        }
      });
    },
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
