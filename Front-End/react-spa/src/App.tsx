import React from 'react';
import { Route } from 'react-router';
import { BrowserRouter } from 'react-router-dom';
import { Provider } from 'mobx-react';
import logo from './logo.svg';
import './App.css';

import { setupRootStore } from './mst/setup';
import ProductCatalogueComponent from './components/ProductCatalogue';
import { AuthComponent } from './components/Auth';
import { LoginComponent } from './components/Login';
import CallBackComponent from './components/Callback';

interface Props {

}

interface State {
  rootTree: any
}


class App extends React.Component<Props, State> {

  constructor(props: Props) {
    super(props);

    this.state = {
      rootTree: null
    };
  }

  componentDidMount = () => {
    const { rootTree } = setupRootStore();

    this.setState({ rootTree })
  }

  render() {

    const { rootTree } = this.state

    if (!rootTree) return null;

    return (
      <Provider rootTree={rootTree}>
        <BrowserRouter>
          <AuthComponent />
          <Route
            exact
            path='/'
            render={() => <ProductCatalogueComponent />}
          />
          <Route
            exact
            path='/login'
            render={() => <LoginComponent />}
          />
           <Route
            exact
            path='/callback'
              render={() => <CallBackComponent />}
          />
        </BrowserRouter>
      </Provider>
    );
  }

}

export default App;
