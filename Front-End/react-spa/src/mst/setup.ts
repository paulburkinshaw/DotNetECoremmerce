import { RootModel } from "."
import { onSnapshot, getSnapshot, applySnapshot } from "mobx-state-tree";

import makeInspectable from 'mobx-devtools-mst';

export const setupRootStore = () => {
    
    const rootTree = RootModel.create({
        auth: {
            auth0: {},
            accessToken: '',
            loading: true,
            authenticated: false
        },
        productCatalogue: {
            products: []
        }
    });

    makeInspectable(rootTree);

    onSnapshot(rootTree, (snapshot) => console.log('snapshot: ', snapshot))

    return { rootTree };
};