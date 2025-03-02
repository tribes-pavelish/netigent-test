import React, { useEffect, useState } from "react";
import { fetchApplications, deleteApplication } from "../api";
import { Application } from "../types";
import { useNavigate } from "react-router-dom";

const Home: React.FC = () => {
    const [applications, setApplications] = useState<Application[]>([]);
    const navigate = useNavigate();

    useEffect(() => {
        fetchApplications()
            .then(response => setApplications(response))
            .catch(error => console.error("Error fetching applications", error));
    }, []);

    const handleDelete = (id: number) => {
        if (window.confirm("Are you sure you want to delete this application?")) {
            deleteApplication(id).then(() => {
                setApplications(applications.filter(app => app.id !== id));
            }).catch(error => console.error("Error deleting application", error));
        }
    };

    return (
        <div className="container w-full mx-auto p-6">
            <h2 className="text-2xl font-bold mb-4">Customer X - Application Tracker</h2>
            <button className="bg-blue-500 text-white px-4 py-2 rounded-md mb-4" onClick={() => navigate("/edit/new")}>Add Application</button>

            <div className="overflow-x-auto">
                <table className="w-full border-collapse border border-gray-300 shadow-lg">
                    <thead className="bg-gray-200">
                        <tr>
                            <th className="border px-4 py-2">#</th>
                            <th className="border px-4 py-2">Project Name</th>
                            <th className="border px-4 py-2">Location</th>
                            <th className="border px-4 py-2">Status</th>
                            <th className="border px-4 py-2">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {applications.length > 0 ? (
                            applications.map((app, index) => (
                                <tr key={app.id} className="hover:bg-gray-100">
                                    <td className="border px-4 py-2">{index + 1}</td>
                                    <td className="border px-4 py-2">{app.projectName || "Unknown"}</td>
                                    <td className="border px-4 py-2">{app.projectLocation || "N/A"}</td>
                                    <td className="border px-4 py-2">{app.appStatus || "New"}</td>
                                    <td className="border px-4 py-2 space-x-2">
                                        <button className="bg-blue-500 text-white px-3 py-1 rounded-md" onClick={() => navigate(`/edit/${app.id}`)}>Edit</button>
                                        <button className="bg-red-500 text-white px-3 py-1 rounded-md" onClick={() => handleDelete(app.id)}>Delete</button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={5} className="text-center py-4">No items</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        </div>
    );
};

export default Home;
