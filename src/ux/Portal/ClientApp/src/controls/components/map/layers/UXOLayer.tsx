import * as React from "react";
import { loadModules } from "@esri/react-arcgis";
import { ISelectedItem } from "../../../../models/Map/ISelectedItem";
import { IMapItem } from "../../../../models/Map/IMapItem";
import { Configuration } from "../../../../tools/utils/config";

export interface ILayerProps {
    map?: __esri.Map;
    view?: __esri.View;
}

export interface UXOLayerProps extends ILayerProps {
    items: IMapItem[] | null;
    onUxoSelected(item: ISelectedItem): void;
}

export class UXOLayer extends React.Component<UXOLayerProps> {
    private symbols: Map<string, any>;

    constructor(props: UXOLayerProps) {
        super(props);
        this.symbols = new Map<string, any>();
    }

    componentWillMount() {
        if (this.props && this.props.items) {
            this.loadData();
        }
    }

    private loadData() {
        loadModules([
            "esri/core/Collection",
            "esri/Graphic",
            "esri/layers/GraphicsLayer",
            "esri/geometry/Point",
            "esri/symbols/PictureMarkerSymbol"
        ]).then(([Collection, Graphic, GraphicsLayer, Point, PictureMarkerSymbol]) => {
            this.buildFeatureLayer(Collection, Graphic, GraphicsLayer, Point, PictureMarkerSymbol);
        });
    }

    private buildFeatureLayer = (Collection: any, Graphic: any, GraphicsLayer: any, Point: any, PictureMarkerSymbol: any) => {
        if (this.props.items && this.props.items.length > 0 && this.props.map && this.props.view) {
            let graphics = (new Collection()) as __esri.Collection;
            for (let itemCount = 0; itemCount < this.props.items.length; itemCount++) {
                const item = this.props.items[itemCount];
                let graphic = (new Graphic()) as __esri.Graphic;
                const point = (new Point()) as __esri.Point;
                point.y = Number(item.latitude);
                point.x = Number(item.longitude);
                graphic.attributes = {
                    ObjectId: item.id,
                    symbol: item.symbol
                };
                if (!this.symbols.has(item.symbol)) {
                    let symbol = new PictureMarkerSymbol() as __esri.PictureMarkerSymbol;
                    symbol.url = `${Configuration.getInstance().getIconBaseUrl()}${item.symbol}.png`;
                    symbol.width = 40;
                    symbol.height = 40;
                    this.symbols.set(item.symbol, symbol);
                }
                graphic.geometry = point;
                graphic.symbol = this.symbols.get(item.symbol);
                graphics.add(graphic);
            }

            const graphicsLayer = new GraphicsLayer() as __esri.GraphicsLayer;
            graphicsLayer.graphics = graphics;

            this.props.map.layers.add(graphicsLayer);

            (this.props.view as any).on("click", this.onClick);
        }
    }

    private onClick = (ev: any) => {
        if (this.props.view && this.props.map) {
            (this.props.view as any).hitTest(ev.screenPoint).then((response: any) => {
                if (response && response.results && response.results[0] && response.results[0].graphic) {
                    var graphic = response.results[0].graphic;
                    graphic.popupTemplate = null;
                    ev.stopPropagation();
                    this.props.onUxoSelected({
                        type: "ied",
                        id: graphic.uid
                    });
                }
            });
        }
    }

    public render() {
        return null;
    }
}