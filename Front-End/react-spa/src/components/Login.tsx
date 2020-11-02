import * as React from "react";
import { observer, inject } from "mobx-react";
import { Root } from "../mst";

import createAuth0Client from '@auth0/auth0-spa-js';

interface LoginComponentProps {
    rootTree?: Root;
}

interface LoginComponentState {

}

@inject("rootTree")
@observer
class LoginComponent extends React.Component<LoginComponentProps, LoginComponentState> {

    constructor(props: LoginComponentProps) {
        super(props);

        this.state = {

        };
    }

    OnLoginButtonClick = (rootTree: any) => {
        if(!rootTree.auth.authenticated){
            rootTree.auth.auth0.loginWithRedirect();
        } else{
            rootTree.auth.auth0.logout();
            rootTree.auth.setAuth(null, false);
        }
    }

    render() {
        const { rootTree } = this.props;
        if (!rootTree) return null;

        return (
            <div className='row'>
                <div className='col-md-4 offset-md-4'>
                    <div className='login'>
                        <button disabled={rootTree.auth.loading} className='btn btn-primary' onClick={() => this.OnLoginButtonClick(rootTree)}>
                            {rootTree.auth.loading ? <i className="fa fa-gear fa-spin" /> : null} 
                            {!rootTree.auth.authenticated ? <span>Login</span> : <span>Logout</span>} 
                        </button>
                    </div>
                </div>
            </div>
        );
    }
}

export { LoginComponent }
