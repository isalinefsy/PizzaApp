import { ComposableFactory, ServiceFactory } from "@dative-gpi/bones-ui";

class Pizza {
  id: number;
  name: string;
  isGlutenFree: boolean;

  constructor(dto: PizzaDTO) {
    this.id = dto.id;
    this.name = dto.name;
    this.isGlutenFree = dto.isGlutenFree;
  }
}

interface PizzaDTO {
    id: number;
  name: string;
  isGlutenFree: boolean;
}


const PizzaFactory = new ServiceFactory<PizzaDTO, Pizza>("pizza", Pizza).create(
    (factory) =>
      factory.build(
        factory.addGetMany<PizzaDTO, Pizza, null>(
          () => "http://localhost:5227/pizza",
          Pizza
        ),
        factory.addCreate<Pizza>("http://localhost:5227/pizza"),
        factory.addRemove((id) => `http://localhost:5227/pizza/${id}`),
        factory.addNotify()
      )
  );
  
  export const usePizzas = ComposableFactory.getMany(PizzaFactory);
  export const removePizza = ComposableFactory.remove(PizzaFactory);
  export const addPizza = ComposableFactory.create(PizzaFactory);
  export { Pizza };
