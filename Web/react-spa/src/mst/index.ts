import { types, Instance, applySnapshot, flow, onSnapshot } from 'mobx-state-tree';
import { values } from "mobx"
import api from 'axios';


const ProductModel = types.model("Product", {
    id: types.integer,
    name: types.string,
    description: types.string,
    price: types.number
})

export const ProductCatalogueModel = types.model("ProductCatalogue", {
    // products: types.map(ProductModel) // types.array(ProductModel)
    products: types.array(ProductModel)
})
    .actions(self => ({

        afterCreate() {
            this.fetchProductsFromApi()
        },
        fetchProductsFromApi: flow(function* fetchProductsFromApi() {

            try {
               
                const response = yield api.get('https://localhost:5001/api/v1/products');

                const responseJson = response.data;

                const products = responseJson.map((productJson: any) => {
                    return {
                        id: productJson.id,
                        name: productJson.name,
                        description: productJson.description ? productJson.description : '',
                        price: productJson.price
                    }
                })

                applySnapshot(self.products, products);

            } catch (error) {
                console.error("Failed to fetch products", error)
            }
        })

    }))
    .views(self => ({
        getProducts() {
            return self.products;
        }
    }))


const RootModel = types.model("Root", {
    productCatalogue: ProductCatalogueModel
});

export { RootModel };

export type Root = Instance<typeof RootModel>;
export type ProductCatalogue = Instance<typeof ProductCatalogueModel>;
export type Product = Instance<typeof ProductModel>;

