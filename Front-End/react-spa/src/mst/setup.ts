import { RootModel } from "."
import { onSnapshot, getSnapshot, applySnapshot } from "mobx-state-tree";

import makeInspectable from 'mobx-devtools-mst';

export const setupRootStore = () => {
    
    const rootTree = RootModel.create({
        productCatalogue: {
            products: []
        }
    });

    makeInspectable(rootTree);

    onSnapshot(rootTree, (snapshot) => console.log('snapshot: ', snapshot))

    return { rootTree };
};