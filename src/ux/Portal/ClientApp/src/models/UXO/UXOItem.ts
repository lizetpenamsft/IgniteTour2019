export interface IUXOItem {
    id: string;
    reported: Date;
    location: string;
    reportingUnit: string;
    contactFrequency: string;
    contactCallSign: string;
    contactPhone: string;
    ordinanceText: string;
    nbcContamination: string;
    resourcesThreatened: string;
    missionImpact: string;
    protectiveMeasures: string;
    priorityText: string;
}

export class UXOItem {
    public id: string;
    public reported: Date;
    public location: string;
    public reportingUnit: string;
    public contactFrequency: string;
    public contactCallSign: string;
    public contactPhone: string;
    public contactName: string;
    public ordinanceText: string;
    public nbcContamination: string;
    public resourcesThreatened: string;
    public missionImpact: string;
    public protectiveMeasures: string;
    public priorityText: string;

    constructor() {
        this.id = "";
        this.reported = new Date("0001-01-01T08:00:00+00:00");
        this.location = "";
        this.reportingUnit = "";
        this.contactFrequency = "";
        this.contactCallSign = "";
        this.contactPhone = "";
        this.contactName = "";
        this.ordinanceText = "";
        this.nbcContamination = "";
        this.resourcesThreatened = "";
        this.missionImpact = "";
        this.protectiveMeasures = "";
        this.priorityText = "";
    }
}