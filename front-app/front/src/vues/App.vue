<template>
  <div id="app">
    <FSSpan font="text-h1" :label="'Bienvenue'" align="center-center" />
    <Pizza />
    <Weather />
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import Pizza from './Pizza.vue';
import Weather from './Weather.vue';
import FSSpan from '@dative-gpi/foundation-shared-components/components/FSSpan.vue';

export default defineComponent({
  name: 'App',
  components: {
    Pizza,
    Weather,
    FSSpan
  }
});
</script>
