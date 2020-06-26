import { RootModel } from "."
import { onSnapshot, getSnapshot, applySnapshot } from "mobx-state-tree";


export const setupRootStore = () => {
    
    const rootTree = RootModel.create({
        productCatalogue: {
            products: []
        }
    });

    onSnapshot(rootTree, (snapshot) => console.log('snapshot: ', snapshot))

    return { rootTree };
};