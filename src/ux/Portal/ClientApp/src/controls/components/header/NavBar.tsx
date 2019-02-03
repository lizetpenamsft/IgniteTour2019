import * as React from "react";
import { Link } from "react-router-dom";

export class NavBar extends React.Component {
    public render() {
        return (
            <nav className="navbar">
                <Link to="/" className="navbar-brand ms-font-xl">
                    Ignite Tour App
                </Link>
            </nav>
        );
    }
}