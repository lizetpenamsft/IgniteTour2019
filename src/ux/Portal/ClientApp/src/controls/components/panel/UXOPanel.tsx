import * as React from "react";
import {
    Panel, PanelType,
    Spinner, SpinnerType,
    PrimaryButton,
    Label,
    Pivot,
    PivotItem
} from "office-ui-fabric-react";
import { UXOItem } from "../../../models/UXO/UXOItem";

export interface UXOPanelProps {
    isOpen: boolean;
    isLoadingItem: boolean;
    isGeneratingDocument: boolean;
    uxo: UXOItem;

    panelClosedAction(): void;
    generateDocumentAction(): void;
}

export class UXOPanel extends React.Component<UXOPanelProps> {
    constructor(props: UXOPanelProps) {
        super(props);
    }

    public render() {
        return (
            <Panel
                isOpen={this.props.isOpen}
                type={PanelType.smallFixedFar}
                onDismiss={this.onClosePanel}
                headerText="IED / UXO"
                closeButtonAriaLabel="Close"
                isFooterAtBottom={true}
                onRenderFooterContent={this.renderGenerateButton}
            >
                {this.renderLoading()}
                {this.renderUXOContent()}
            </Panel>
        );
    }

    private onClosePanel = () => {
        this.props.panelClosedAction();
    }

    private onGenerateDocument = () => {
        this.props.generateDocumentAction();
    }

    private renderLoading() {
        const spinnerContent = (
            <Spinner type={SpinnerType.large} />
        );
        return this.props.isLoadingItem ? spinnerContent : null;
    }

    private renderGenerateButton = () => {
        const buttonContent = (
            <div>
                <PrimaryButton
                    disabled={this.props.isGeneratingDocument || this.props.isLoadingItem}
                    onClick={this.onGenerateDocument}
                >
                    Generate
                </PrimaryButton>
            </div>
        );
        return buttonContent;
    }

    private renderUXOContent() {
        let content;
        if (!this.props.isLoadingItem && this.props.uxo) {
            content = (
                <div>
                    <Pivot>
                        <PivotItem
                            headerText="Overview"
                        >
                            <div>
                                <Label className="ms-fontsize-xl ms-fontWeight-semibold">Type</Label>
                                <Label>{this.props.uxo.ordinanceText}</Label>
                                <Label className="ms-fontsize-xl ms-fontWeight-semibold">Priority</Label>
                                <Label>{this.props.uxo.priorityText}</Label>
                                <Label className="ms-fontsize-xl ms-fontWeight-semibold">Reporting Unit</Label>
                                <Label>{this.props.uxo.reportingUnit}</Label>
                                <Label className="ms-fontsize-xl ms-fontWeight-semibold">Time</Label>
                                <Label>{this.props.uxo.reported.toString()}</Label>
                                <Label className="ms-fontsize-xl ms-fontWeight-semibold">Location</Label>
                                <Label>{this.props.uxo.location}</Label>
                            </div>
                        </PivotItem>
                        <PivotItem
                            headerText="Contact"
                        >
                            <div>
                                <Label className="ms-fontsize-xl ms-fontWeight-semibold">Frequency</Label>
                                <Label>{this.props.uxo.contactFrequency}</Label>
                                <Label className="ms-fontsize-xl ms-fontWeight-semibold">Call Sign</Label>
                                <Label>{this.props.uxo.contactCallSign}</Label>
                                <Label className="ms-fontsize-xl ms-fontWeight-semibold">Phone</Label>
                                <Label>{this.props.uxo.contactPhone}</Label>
                                <Label className="ms-fontsize-xl ms-fontWeight-semibold">Name</Label>
                                <Label>{this.props.uxo.contactName}</Label>
                            </div>
                        </PivotItem>
                        <PivotItem
                            headerText="Impact"
                        >
                            <div>
                                <Label className="ms-fontsize-xl ms-fontWeight-semibold">NBC Contamination</Label>
                                <Label>{this.props.uxo.nbcContamination}</Label>
                                <Label className="ms-fontsize-xl ms-fontWeight-semibold">Resources Threatened</Label>
                                <Label>{this.props.uxo.resourcesThreatened}</Label>
                                <Label className="ms-fontsize-xl ms-fontWeight-semibold">Impact on Mission</Label>
                                <Label>{this.props.uxo.missionImpact}</Label>
                                <Label className="ms-fontsize-xl ms-fontWeight-semibold">Protective Measures Taken</Label>
                                <Label>{this.props.uxo.protectiveMeasures}</Label>
                            </div>
                        </PivotItem>
                    </Pivot>
                </div>
            );
        } else {
            content = null;
        }
        return content;
    }
}