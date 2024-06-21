<template>
  <div id="app">
    <FSSpan font="text-h1" :label="'Bienvenue'" align="center-center" />
    <Pizza />
    <Weather />
    <SankeyDiagram />
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import Pizza from './Pizza.vue';
import Weather from './Weather.vue';
import FSSpan from '@dative-gpi/foundation-shared-components/components/FSSpan.vue';
import SankeyDiagram from './SankeyDiagram.vue';

export default defineComponent({
  name: 'App',
  components: {
    Pizza,
    Weather,
    FSSpan, 
    SankeyDiagram
  }
});
</script>
