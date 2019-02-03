import { UXOItem } from "./UXOItem";
import { IMapItem } from "../Map/IMapItem";

export interface IUXOView {
    selectedItem: UXOItem;
    isLoading: boolean;
    isGeneratingDocument: boolean;
    buttonText: string;
    items: IMapItem[];
}