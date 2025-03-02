import { Application, Inquiry, StatusLevel } from "@/types";

const API_URL = "https://localhost:56219/";

const request = async <T>(
    url: string,
    method: string = "GET",
    body?: unknown
): Promise<T> => {
    const options: RequestInit = {
        method,
        headers: { "Content-Type": "application/json" },
        body: body ? JSON.stringify(body) : undefined,
    };
    const response = await fetch(API_URL + url, options);
    if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
    }
    return response.json();
};

export const fetchApplications = () => request<Application[]>(`api/application`);
export const getApplicationById = (id: number) => request<Application>(`api/application/${id}`);
export const createApplication = (data: Partial<Application>) => request<Application>(`api/application`, "POST", data);
export const updateApplication = (id: number, data: Partial<Application>) => request(`api/application/${id}`, "PUT", data);
export const deleteApplication = (id: number) => request(`api/application/${id}`, "DELETE");

export const fetchInquiries = (appId: number) => request<Inquiry[]>(`api/application/${appId}/inquires`);
export const addInquiry = (data: Partial<Inquiry>) => request<Inquiry>(`api/inquire`, "POST", data);
export const deleteInquiry = (id: number) => request(`api/inquire/${id}`, "DELETE");

export const fetchStatusLevels = () => request<StatusLevel[]>(`api/statuslevel`);