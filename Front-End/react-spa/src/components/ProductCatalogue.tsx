import * as React from "react";
import { inject, observer } from "mobx-react";
import { Root, Product } from "../mst";
import { ProductComponent } from "./Product";

interface ProductCatalogueComponentProps {
    rootTree?: Root;
}

interface ProductCatalogueComponentState {

}

@inject("rootTree")
@observer
class ProductCatalogueComponent extends React.Component<ProductCatalogueComponentProps, ProductCatalogueComponentState> {

    constructor(props: ProductCatalogueComponentProps) {
        super(props);
        this.state = {

        };
    }

    render() {

        const { rootTree } = this.props;
        if (!rootTree) return null;

        const products = rootTree.productCatalogue.getProducts();

        return (
            <div>
                <h1>Product Catalogue</h1>

                <hr />

                <table>
                    <thead>
                        <tr>
                            <th>Product Name</th>
                            <th>Product Description</th>
                            <th>Product Price</th>
                        </tr>
                    </thead>
                    <tbody>
                        {products.map(product => (
                            <ProductComponent product={product} key={product.id} />
                        ))}
                    </tbody>
                </table>
            </div>
        )

    }

}

export { ProductCatalogueComponent };