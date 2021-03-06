import * as React from "react";
import { withRouter } from "react-router";
import { RouteComponentProps } from "react-router-dom";
import { observer, inject } from "mobx-react";
import createAuth0Client from '@auth0/auth0-spa-js';
import { History, LocationState } from "history";

import { Root } from "../mst";


interface CallBackComponentProps extends RouteComponentProps {
    rootTree?: Root;
    history: History<LocationState>;
}

interface CallBackComponentState {

}

@inject("rootTree")
@observer
class CallBackComponent extends React.Component<CallBackComponentProps, CallBackComponentState> {

    constructor(props: CallBackComponentProps) {
        super(props);

        this.state = {

        };
    }

    componentWillUpdate = async (nextProps) => {

        const { rootTree } = this.props;
        if (!rootTree) return null;

        await rootTree.auth.auth0.handleRedirectCallback();
        let token = await rootTree.auth.auth0.getTokenSilently();
        
        rootTree.auth.setAuth(token, true);
        this.props.history.push('/');

    }


    render() {
        const { rootTree } = this.props;
        if (!rootTree) return null;

        return (
            <div>Loading user profile. {
                rootTree.auth.auth0.loading ?
                    <i style={{ color: 'black' }} className="fa fa-gear fa-spin"></i>
                    : null
            }
            </div>
        )
    }
}

export default withRouter(CallBackComponent) 
