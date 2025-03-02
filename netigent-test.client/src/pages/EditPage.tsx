import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getApplicationById, createApplication, updateApplication, fetchInquiries, addInquiry, deleteInquiry, fetchStatusLevels } from "../api";
import { Application, Inquiry, StatusLevel } from "../types";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

const EditPage: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();
    const [application, setApplication] = useState<Partial<Application>>({});
    const [inquiries, setInquiries] = useState<Inquiry[]>([]);
    const [newInquiry, setNewInquiry] = useState("");
    const [statusLevels, setStatusLevels] = useState<StatusLevel[]>([]);

    useEffect(() => {
        if (id !== "new") {
            getApplicationById(Number(id)).then(response => setApplication(response));
            fetchInquiries(Number(id)).then(response => setInquiries(response));
        }
        fetchStatusLevels().then(response => setStatusLevels(response));
    }, [id]);

    useEffect(() => {
        if (id !== "new") {
            getApplicationById(Number(id)).then(response => setApplication(response));
            fetchInquiries(Number(id)).then(response => setInquiries(response));
        }
    }, [id]);

    const handleSave = () => {
        if (id === "new") {
            createApplication(application).then(() => navigate("/"));
        } else {
            updateApplication(Number(id), application).then(() => navigate("/"));
        }
    };

    const handleAddInquiry = () => {
        if (newInquiry.trim()) {
            addInquiry({ applicationId: Number(id), inquiry: newInquiry })
                .then(response => setInquiries([...inquiries, response]))
                .catch(error => console.error("Error adding inquiry", error));
        }
    };

    const handleDeleteInquiry = (inquiryId: number) => {
        deleteInquiry(inquiryId).then(() => {
            setInquiries(inquiries.filter(i => i.id !== inquiryId));
        });
    };

    return (
        <div className="container mx-auto p-6">
            <h2 className="text-2xl font-bold mb-4">Customer X - Application Tracker</h2>

            {/* Application Form */}
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                <div className="bg-white p-4 shadow rounded-md">
                    <label className="block font-medium">Project Name</label>
                    <input type="text" className="w-full border p-2 rounded"
                        value={application.projectName || ""}
                        onChange={(e) => setApplication({ ...application, projectName: e.target.value })}
                    />

                    <label className="block font-medium mt-2">Project Location</label>
                    <input type="text" className="w-full border p-2 rounded"
                        value={application.projectLocation || ""}
                        onChange={(e) => setApplication({ ...application, projectLocation: e.target.value })}
                    />

                    <label className="block font-medium mt-2">Project Ref</label>
                    <input type="text" className="w-full border p-2 rounded"
                        value={application.projectRef || ""}
                        onChange={(e) => setApplication({ ...application, projectRef: e.target.value })}
                    />

                    <label className="block font-medium mt-2">Project Value</label>
                    <input type="number" className="w-full border p-2 rounded"
                        value={application.projectValue || 0}
                        onChange={(e) => setApplication({ ...application, projectValue: parseFloat(e.target.value) })}
                    />

                    {/* Date Pickers */}
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-4 py-2">
                        {/* Open Date */}
                        <div>
                            <label className="block font-medium">Open Date</label>
                            <DatePicker
                                selected={application.openDt ? new Date(application.openDt) : null}
                                onChange={(date) => setApplication({ ...application, openDt: date?.toISOString() || "" })}
                                className="w-full border p-2 rounded"
                                dateFormat="yyyy-MM-dd"
                                placeholderText="Select Open Date"
                            />
                        </div>

                        {/* Start Date */}
                        <div>
                            <label className="block font-medium">Start Date</label>
                            <DatePicker
                                selected={application.startDt ? new Date(application.startDt) : null}
                                onChange={(date) => setApplication({ ...application, startDt: date?.toISOString() || "" })}
                                className="w-full border p-2 rounded"
                                dateFormat="yyyy-MM-dd"
                                placeholderText="Select Start Date"
                            />
                        </div>

                        {/* Completed Date */}
                        <div>
                            <label className="block font-medium">Completed Date</label>
                            <DatePicker
                                selected={application.completeDt ? new Date(application.completeDt) : null}
                                onChange={(date) => setApplication({ ...application, completeDt: date?.toISOString() || "" })}
                                className="w-full border p-2 rounded"
                                dateFormat="yyyy-MM-dd"
                                placeholderText="Select Completed Date"
                            />
                        </div>

                        {/* Status Dropdown */}
                        <div>
                            <label className="block font-medium">Status</label>
                            <select className="w-full border p-2 rounded"
                                value={application.statusId || ""}
                                onChange={(e) => setApplication({
                                    ...application,
                                    appStatus: statusLevels.find(item => item.id === Number(e.target.value))?.statusName,
                                    statusId: Number(e.target.value)
                                })}
                            >
                                <option value="" disabled>Select Status</option>
                                {statusLevels.map(status => (
                                    <option key={status.id} value={status.id}>{status.statusName}</option>
                                ))}
                            </select>
                        </div>
                    </div>

                    <label className="block font-medium mt-2">Notes</label>
                    <textarea className="w-full border p-2 rounded" rows={3}
                        value={application.notes || ""}
                        onChange={(e) => setApplication({ ...application, notes: e.target.value })}
                    ></textarea>

                    <button className="bg-green-500 text-white px-4 py-2 rounded-md mt-4 w-full" onClick={handleSave}>
                        Save Application
                    </button>
                </div>

                {/* Inquiries Section */}
                <div className="bg-white p-4 shadow rounded-md">
                    <h3 className="text-lg font-semibold mb-2">Inquiries</h3>
                    <div className="space-y-2">
                        {inquiries.length > 0 ? (
                            inquiries.map((inquiry) => (
                                <div key={inquiry.id} className="border p-3 rounded flex justify-between items-center">
                                    <span>{inquiry.inquiry}</span>
                                    <button className="text-red-500" onClick={() => handleDeleteInquiry(inquiry.id)}>X</button>
                                </div>
                            ))
                        ) : (
                            <p className="text-gray-500">No inquiries found</p>
                        )}
                    </div>

                    <div className="mt-4">
                        <input type="text" className="w-full border p-2 rounded"
                            placeholder="New Inquiry"
                            value={newInquiry}
                            onChange={(e) => setNewInquiry(e.target.value)}
                        />
                        <button className="bg-blue-500 text-white px-4 py-2 rounded-md mt-2 w-full" onClick={handleAddInquiry}>
                            Add Inquiry
                        </button>
                    </div>
                </div>
            </div>

            <div className="flex justify-between mt-6">
                <button className="bg-gray-500 text-white px-4 py-2 rounded-md" onClick={() => navigate("/")}>Cancel</button>
            </div>
        </div>
    );
};

export default EditPage;
