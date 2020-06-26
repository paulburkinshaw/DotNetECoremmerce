import * as React from "react";
import { inject, observer } from "mobx-react";
import { Root } from "../mst";

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

        return (
            <div>
                <h1>Product Catalogue</h1>
            </div>
        )

    }

}

export { ProductCatalogueComponent };