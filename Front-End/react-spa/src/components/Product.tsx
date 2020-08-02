import * as React from "react";
import { Product } from "../mst";
import { observer, inject } from "mobx-react";
import { Root } from "../mst";

interface ProductComponentProps {
    product: Product;
}

interface ProductComponentState {
    name: string;
    description: string;
    price: string;
    category: string;
}

@inject("rootTree")
@observer
class ProductComponent extends React.Component<ProductComponentProps, ProductComponentState> {

    constructor(props: ProductComponentProps) {
        super(props);

        this.state = {
            name: this.props.product.name,
            description: `${this.props.product.description}`,
            category: `${this.props.product.category}`,
            price: `${this.props.product.price}`
        };

    }

    render() {
        const { name, description, category, price } = this.props.product;

        return (

            <tr>
                <td>
                    {`${name}`}
                </td>
                <td>
                    {`${category}`}
                </td>
                <td>
                    {`${description}`}
                </td>
                <td>
                    {`${price}`}
                </td>
            </tr>
        )
    }
}

export { ProductComponent }
