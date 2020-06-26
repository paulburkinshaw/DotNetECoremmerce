import { types, Instance, applySnapshot, flow, onSnapshot } from 'mobx-state-tree';
import api from 'axios';


const ProductModel = types.model("Product", {
    id: types.integer,
    name: types.string,
    description: types.string,
    price: types.number
})

const ProductCatalogueModel = types.model("ProductCatalogue", {
    products: types.array(ProductModel)
})

const RootModel = types.model("Root", {
    productCatalogue: ProductCatalogueModel
});

export { RootModel };

export type Root = Instance<typeof RootModel>;
export type ProductCatalogue = Instance<typeof ProductCatalogueModel>;
export type Product = Instance<typeof ProductModel>;

