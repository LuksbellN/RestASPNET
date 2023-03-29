import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Books from "./pages/Books";
import Login from "./pages/Login";
import NewBook from "./pages/NewBook";

export default function Routs(){
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" exact Component={Login}></Route>
                <Route path="/books" Component={Books}></Route>
                <Route path="/book/new/:bookId" Component={NewBook}></Route>
            </Routes>
        </BrowserRouter>
    );
}