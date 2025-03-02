import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Home from "@/pages/Home";
import EditPage from "@/pages/EditPage";
import "bootstrap/dist/css/bootstrap.min.css";

const App: React.FC = () => (
    <Router>
        <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/edit/:id" element={<EditPage />} />
        </Routes>
    </Router>
);

export default App;
