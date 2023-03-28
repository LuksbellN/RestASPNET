import React from "react";

export default function Header({children}) {
    return (
        <header>
            <h1>
                <h1>{children}</h1>
            </h1>
        </header>
    );
}