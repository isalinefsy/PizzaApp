import { createApp } from 'vue';
import App from './vues/App.vue';
import { createVuetify } from 'vuetify';
import 'vuetify/styles';
import { aliases, mdi } from 'vuetify/iconsets/mdi';
import * as components from 'vuetify/components';
import * as directives from 'vuetify/directives';
import { createRouter, createWebHistory } from 'vue-router';
import "@dative-gpi/foundation-shared-components/styles/main.scss";
import "@mdi/font/css/materialdesignicons.css";

// Importez vos composants de page ici
import HomePage from './vues/App.vue';
import PizzaPage from './vues/Pizza.vue';

const routes = [
  { path: '/', component: HomePage },
  { path: '/pizzas', component: PizzaPage },
  // Ajoutez d'autres routes ici
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

const vuetify = createVuetify({
  components,
  directives,
  icons: {
    defaultSet: 'mdi',
    aliases,
    sets: {
      mdi,
    },
  },
});

createApp(App)
  .use(vuetify)
  .use(router)
  .mount('#app');
