<template>
  <FSCol
    id="pizza"
    width="fill"
    height="fill"
    align="top-center"
    padding="20px"
  >
    <FSRow
      width="fill"
      height="hug"
      align="center-left"
    >
      <FSSpan font="text-h3" :label="'Liste des Pizzas'" />
    </FSRow>
    <FSRow
      width="fill"
      height="hug"
      align="center-left"
      padding="8px"
      gap="16px"
    >
      <FSDivider />
      <FSCol align="center-center" width="fill">
        <FSSpan font="text-body" :label="'Name'" />
      </FSCol>
      <FSCol align="center-center" width="fill">
        <FSSpan font="text-body" :label="'Gluten Free'" />
      </FSCol>
      <FSCol align="center-center" width="fill">
        <FSSpan font="text-body" :label="'Remove'" />
      </FSCol>
      <FSDivider />
    </FSRow>
    <FSRow
      v-for="pizza in pizzas"
      :key="pizza.id"
      width="fill"
      height="hug"
      align="center-left"
      padding="8px"
      gap="16px"
    >
      <FSCol align="center-center" width="fill">
        <FSSpan font="text-overline" :label="pizza.name" />
      </FSCol>
      <FSCol align="center-center" width="fill">
        <FSSpan
          font="text-overline"
          :label="pizza.isGlutenFree ? 'Oui' : 'Non'"
        />
      </FSCol>
      <FSCol align="center-center" width="fill">
        <FSButton color="error" @click="deletePizza(pizza.id)">
          <FSSpan> Remove </FSSpan>
          <template #append>
            <FSIcon> mdi-minus-circle-outline </FSIcon>
          </template>
        </FSButton>
      </FSCol>
    </FSRow>
    <FSForm
      @submit="createPizza(newPizza)"
      width="fill"
      height="hug"
      align="center-left"
      padding="16px"
      gap="16px"
    >
      <FSDivider />
      <FSRow
        width="fill"
        height="hug"
        align="center-left"
        gap="16px"
        padding="16px"
      >
        <FSCol align="center-center" width="fill">
          <FSTextField
            label="Nom de la pizza"
            :required="true"
            v-model="newPizza.name"
          />
        </FSCol>
        <FSCol align="center-center" width="fill">
          <FSCheckbox v-model="newPizza.isGlutenFree" />
        </FSCol>
        <FSCol align="center-center" width="fill">
          <FSButton type="submit" color="primary">
            <FSIcon> mdi-plus-circle-outline </FSIcon>
            <FSSpan :label="'Ajouter une pizza'" />
          </FSButton>
        </FSCol>
      </FSRow>
    </FSForm>
  </FSCol>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from "vue";
import {
  Pizza,
  addPizza,
  removePizza,
  usePizzas,
} from "../services/PizzaApiService";

import FSCol from "@dative-gpi/foundation-shared-components/components/FSCol.vue";
import FSRow from "@dative-gpi/foundation-shared-components/components/FSRow.vue";
import FSSpan from "@dative-gpi/foundation-shared-components/components/FSSpan.vue";
import FSButton from "@dative-gpi/foundation-shared-components/components/FSButton.vue";
import FSForm from "@dative-gpi/foundation-shared-components/components/FSForm.vue";
import FSCheckbox from "@dative-gpi/foundation-shared-components/components/FSCheckbox.vue";
import FSIcon from "@dative-gpi/foundation-shared-components/components/FSIcon.vue";
import FSTextField from "@dative-gpi/foundation-shared-components/components/fields/FSTextField.vue";
import FSDivider from "@dative-gpi/foundation-shared-components/components/FSDivider.vue";
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
  setup() {
    const { fetching, getMany, entities: pizzas } = usePizzas();
    const { removing, remove } = removePizza();
    const { creating, create, created: pizza } = addPizza();
    const newPizza = ref({
      name: null,
      isGlutenFree: false,
    });
    const fetchPizzas = async () => {
      try {
        await getMany();
        console.log(pizzas.value);
      } catch (error) {
        console.error("Erreur lors de la récupération des pizzas:", error);
      }
    };
    const deletePizza = async (id) => {
      try {
        await remove(id);
        console.log(`La pizza ${id} a été supprimée.`);
      } catch (error) {
        console.error("Erreur lors de la suppression de la pizza:", error);
      }
    };
    const createPizza = async (maPizza) => {
      try {
        await create(maPizza);
        newPizza.value.name = null;
        newPizza.value.isGlutenFree = false;
        console.log(`La pizza a été ajoutée.`);
      } catch (error) {
        console.error("Erreur lors de l'ajout' de la pizza:", error);
      }
    };

    onMounted(async () => {
      await fetchPizzas();
    });

    return {
      pizzas,
      deletePizza,
      newPizza,
      createPizza,
    };
  },
});
</script>
