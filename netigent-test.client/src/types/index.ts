export interface Application {
    id: number;
    appStatus: string;
    projectRef: string;
    projectName: string;
    projectLocation: string;
    openDt: string;
    startDt: string;
    completeDt: string;
    projectValue: number;
    statusId: number;
    notes: string;
    modified: string;
    isDeleted: boolean;
}

export interface Inquiry {
    id: number;
    applicationId: number;
    sendToPerson: string;
    sendToRole: string;
    sendToPersonId: number;
    subject: string;
    inquiry: string;
    response: string;
    askedDt: string;
    completedDt: string;
}

export interface StatusLevel {
    id: number;
    statusName: string;
}
