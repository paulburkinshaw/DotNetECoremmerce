import * as React from "react";
import { observer, inject } from "mobx-react";

import createAuth0Client from '@auth0/auth0-spa-js';

import { Root } from "../mst";

interface AuthComponentProps {
    rootTree?: Root;
}

interface AuthComponentState {

}

@inject("rootTree")
@observer
class AuthComponent extends React.Component<AuthComponentProps, AuthComponentState> {

    constructor(props: AuthComponentProps) {
        super(props);

        this.state = {

        };
    }

    componentWillMount = async () => {

        const { rootTree } = this.props;
        if (!rootTree) return null;

        let auth0 = await createAuth0Client({
            domain: process.env.REACT_APP_AUTH0_DOMAIN ? process.env.REACT_APP_AUTH0_DOMAIN : '',
            client_id: process.env.REACT_APP_AUTH0_CLIENT_ID ? process.env.REACT_APP_AUTH0_CLIENT_ID : '',
            redirect_uri: process.env.REACT_APP_AUTH0_REDIRECT_URI ? process.env.REACT_APP_AUTH0_REDIRECT_URI : '',
            audience: process.env.REACT_APP_AUTH0_AUDIENCE ? process.env.REACT_APP_AUTH0_AUDIENCE : '', // setting audience tells Auth0 to send back an Access Token
            responseType: 'token id_token',
            scope: 'openid profile'
        });
       
        rootTree.auth.initialize(auth0);
        rootTree.auth.setLoading(false);

        if (rootTree.auth.authenticated) {
            let token = await rootTree.auth.auth0.getTokenSilently();
            if (token) {
                rootTree.auth.setAuth(token, true);
            }    
        }
        

    }

   
    render() {
        return null
    }
}

export { AuthComponent }
