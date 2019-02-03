import * as React from "react";
import { Dispatch, bindActionCreators } from "redux";
import { connect } from "react-redux";
import ActionTypes from "../../actions/ActionTypes";
import { RetrieveUXO } from "../../actions/UXO/RetrieveUXOAction";
import { GenerateUXODocument } from "../../actions/UXO/GenerateUXOAction";
import { Header } from "../components/header/Header";
import { MapView } from "../components/map/map/MapView";
import { UXOPanel } from "../components/panel/UXOPanel";
import { IStoreState } from "../../stores/IStoreState";
import { UXOItem } from "../../models/UXO/UXOItem";
import { ISelectedItem } from "../../models/Map/ISelectedItem";
import { GetAllUXOs } from "../../actions/UXO/GetAllUXOsAction";
import { IMapItem } from "../../models/Map/IMapItem";

interface IAppProps {
    isGeneratingDocument: boolean;
    isLoadingItem: boolean;
    isOpen: boolean;
    uxo: UXOItem;
    uxoMapItems: IMapItem[];

    getUXO(id: string): (dispatch: Dispatch<ActionTypes>) => Promise<UXOItem>;
    generateUXODocument(id: string): (dispatch: Dispatch<ActionTypes>) => Promise<void>;
    getAllUXOs(): (dispatch: Dispatch<ActionTypes>) => Promise<IMapItem[]>;
}

interface IAppState {
    panelOpen: boolean;
}

class App extends React.Component<IAppProps, IAppState> {
    constructor(props: IAppProps) {
        super(props);
        this.state = {
            panelOpen: false
        };
        this.onMapItemSelected = this.onMapItemSelected.bind(this);
        this.generateUXODocument = this.generateUXODocument.bind(this);
    }

    public componentWillMount() {
        this.getAllUXOs();
    }

    public render() {
        return (
            <div className="app">
                <div className="app-header">
                    <Header />
                </div>
                <div className="app-body">
                    <MapView
                        onItemSelected={this.onMapItemSelected}
                        uxoMapItems={this.props.uxoMapItems} />
                </div>
                <UXOPanel
                    isGeneratingDocument={this.props.isGeneratingDocument}
                    isLoadingItem={this.props.isLoadingItem}
                    isOpen={this.state.panelOpen}
                    uxo={this.props.uxo}
                    generateDocumentAction={this.generateUXODocument}
                    panelClosedAction={this.closePanel}
                />
            </div>
        );
    }

    private async getAllUXOs() {
        await this.props.getAllUXOs();
    }

    private async generateUXODocument() {
        await this.props.generateUXODocument(this.props.uxo.id);
    }

    private closePanel = () => {
        this.setState({ panelOpen: false });
    }

    private async onMapItemSelected(item: ISelectedItem) {
        this.setState({ panelOpen: true });
        await this.props.getUXO(item.id);
    }
}

function mapStateToProps(state: IStoreState) {
    return {
        isGeneratingDocument: state.uxo.isGeneratingDocument,
        isLoadingItem: state.uxo.isLoading,
        uxo: state.uxo.selectedItem,
        uxoMapItems: state.uxo.items
    };
}

function mapDispatchToProps(dispatch: Dispatch<ActionTypes>) {
    return {
        getUXO: bindActionCreators(RetrieveUXO, dispatch),
        generateUXODocument: bindActionCreators(GenerateUXODocument, dispatch),
        getAllUXOs: bindActionCreators(GetAllUXOs, dispatch)
    };
}

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(App as any);