import * as React from "react";
import { observer, inject } from "mobx-react";
import { Root } from "../mst";

import createAuth0Client from '@auth0/auth0-spa-js';

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
            domain: process.env.REACT_APP_DOMAIN ? process.env.REACT_APP_DOMAIN : '',
            client_id: process.env.REACT_APP_CLIENT_ID ? process.env.REACT_APP_CLIENT_ID : '',
            redirect_uri: process.env.REACT_APP_REDIRECT_URI ? process.env.REACT_APP_REDIRECT_URI : '',
            responseType: 'token id_token',
            scope: 'openid profile'
        });
       
        rootTree.auth.initialize(auth0);
        rootTree.auth.setLoading(false);

    }

   
    render() {


        return null
    }
}

export { AuthComponent }
