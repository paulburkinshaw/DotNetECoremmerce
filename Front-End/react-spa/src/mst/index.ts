import { types, Instance, applySnapshot, flow, onSnapshot } from 'mobx-state-tree';
import { values } from "mobx"
import api from 'axios';

const AuthModel = types.model("Auth", {
    auth0: types.maybe(types.frozen()),
    accessToken: types.string,
    loading: types.boolean,
    authenticated: types.boolean
})
    .actions(self => ({

        initialize(auth0) {
            applySnapshot(self,
                {
                    ...self, auth0: auth0
                });
        },
        setLoading(loading) {
            applySnapshot(self,
                {
                    ...self, loading: loading
                });
        },
        setAuth(token: string, authenticated: boolean) {

            const bearerToken = "Bearer " + token;
            api.defaults.headers.common['Authorization'] = bearerToken;
            
            applySnapshot(self,
                {
                    ...self, accessToken: token, authenticated 
                });
        }

    }))

const ProductModel = types.model("Product", {
    id: types.integer,
    name: types.string,
    description: types.string,
    category: types.string,
    price: types.number
})

const ProductCatalogueModel = types.model("ProductCatalogue", {
    products: types.array(ProductModel)
})
    .actions(self => {
        function newProduct(name: string, description: string, category: string, price: number) {
            const id =0;
            applySnapshot(self,
                {
                    ...self, products: [{ id, name, description, category, price }, ...self.products]
                });

            saveProduct({ id, name, description, category, price, });
        }
        const saveProduct = flow(function* saveProduct(snapshot: any) {
            let product_catalogue_api_url: string = process.env.REACT_APP_PRODUCT_CATOLOGUE_API_URL ? process.env.REACT_APP_PRODUCT_CATOLOGUE_API_URL : '';

            try {

                const response = yield api.post(product_catalogue_api_url, snapshot);

            } catch (e) {
                console.log('error:', e);
            }
        })
        function afterCreate() {

            fetchProductsFromApi();

        }
        const fetchProductsFromApi = flow(function* fetchProductsFromApi() {

            try {

                let product_catalogue_api_url: string = process.env.REACT_APP_PRODUCT_CATOLOGUE_API_URL ? process.env.REACT_APP_PRODUCT_CATOLOGUE_API_URL : '';

                const response = yield api.get(product_catalogue_api_url);

                const responseJson = response.data;

                const products = responseJson.map((productJson: any) => {
                    return {
                        id: productJson.id,
                        name: productJson.name,
                        category: productJson.category ?  productJson.category: '',
                        description: productJson.description ? productJson.description : '',
                        price: productJson.price
                    }
                })

                applySnapshot(self.products, products);

            } catch (error) {
                console.error("Failed to fetch products", error)
            }
        })
        return { newProduct, fetchProductsFromApi, afterCreate, saveProduct };
    })
    .views(self => ({
        getProducts() {
            return self.products;
        }
    }))


const RootModel = types.model("Root", {
    auth: AuthModel,
    productCatalogue: ProductCatalogueModel   
});

export { RootModel };

export type Root = Instance<typeof RootModel>;
export type ProductCatalogue = Instance<typeof ProductCatalogueModel>;
export type Product = Instance<typeof ProductModel>;
export type Auth = Instance<typeof AuthModel>;

