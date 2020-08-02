import * as React from "react";
import { inject, observer } from "mobx-react";
import { History, LocationState } from "history";
import { withRouter } from "react-router";
import { RouteComponentProps } from "react-router-dom";

import { Root, Product } from "../mst";
import { ProductComponent } from "./Product";


interface ProductCatalogueComponentProps extends RouteComponentProps {
    rootTree?: Root;
    history: History<LocationState>;
}

interface ProductCatalogueComponentState {
    productName: string;
    productDescription: string;
    productCategory: string;
    productPrice: string;
}

@inject("rootTree")
@observer
class ProductCatalogueComponent extends React.Component<ProductCatalogueComponentProps, ProductCatalogueComponentState> {

    constructor(props: ProductCatalogueComponentProps) {
        super(props);

        this.state = {
            productName: "",
            productDescription: "",
            productCategory: "",
            productPrice: "",
        };
    }

    changeProductName = (e: any) => {
        const productName = e.target.value;
        this.setState({ productName });
    }

    changeProductDescription = (e: any) => {
        const productDescription = e.target.value;
        this.setState({ productDescription });
    }

    changeProductCategory = (e: any) => {
        const productCategory = e.target.value;
        this.setState({ productCategory });
    }

    changeProductPrice = (e: any) => {
        const productPrice = e.target.value;
        this.setState({ productPrice });
    }

    onSubmit = (e: any) => {
        e.preventDefault();

        const { productName, productDescription, productCategory, productPrice } = this.state;

        const { rootTree } = this.props;

        if (!rootTree) return null;

        const bearerToken = "Bearer " + rootTree.auth.accessToken;
        rootTree.productCatalogue.newProduct(productName, productDescription, productCategory, parseInt(productPrice), bearerToken);
        
        this.setState({productName: "", productDescription: "", productCategory: "", productPrice: ""})

    }

    render() {

        const { rootTree } = this.props;

        const { productName, productDescription, productCategory, productPrice } = this.state;

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

                {rootTree.auth.authenticated &&
                    <>
                        <hr />
                        <p>New Product</p>
                        <form onSubmit={this.onSubmit}>
                            <p>Name: </p>
                            <input value={productName} onChange={this.changeProductName} />
                            <p>Description: </p>
                            <input value={productDescription} onChange={this.changeProductDescription} />
                            <p>Category: </p>
                            <input value={productCategory} onChange={this.changeProductCategory} />
                            <p>Price: </p>
                            <input value={productPrice} onChange={this.changeProductPrice} />
                            <br />
                            <button type="submit">Submit</button>
                        </form>
                        <hr />
                    </>

                }

            </div>
        )

    }

}

export default withRouter(ProductCatalogueComponent) 
