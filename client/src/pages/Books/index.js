import React, {useEffect, useState} from "react";
import { Link, useNavigate } from "react-router-dom";
import "./styles.css";
import logoImage from '../../assets/logo.svg';
import { FiPower, FiEdit, FiTrash2 } from 'react-icons/fi'
import api from '../../services/api';

export default function Books() {

    const [books, setBooks] = useState([]);
    const [page, setPage] = useState(1)

    const userName = localStorage.userName;
    const accessToken = localStorage.accessToken;

    const navigate = useNavigate();

    const config = {
        headers:{
            Authorization: `Bearer ${accessToken}`
        }
    };

    useEffect(() => {
        fetchMoreBooks();
    }, [accessToken]);


    async function fetchMoreBooks(){
        const response = await api.get(`v1/api/book/asc/4/${page}`, config);
        setBooks([...books, ...response.data.data]);
        setPage(page + 1);
    };
    

    async function logout() {
        try{
            await api.get('v1/api/auth/revoke', config);
            localStorage.clear();
            navigate('/');

        } catch(error){

        }
    }

    async function editBook(id){
        try{
            navigate(`/book/new/${id}`);
        } catch(error){
            alert('Edit failed!')
        }
    }

    async function deleteBook(id) {
        try{
            await api.delete(`v1/api/book/${id}`, config);
            setBooks(books.filter(book => book.id !== id))
        } catch(error){
            alert('Delete Failed!')
        }
    }

    return (
        <div className="book-container">
            <header>
                <img src={logoImage} alt="ErudioLogo" />
                <span>Welcome, <strong>{userName.toLowerCase()}</strong>!</span>
                <Link className="button" to="/book/new/0">Add new book</Link>
                <button type="button" onClick={logout}>
                    <FiPower size={18} color="#251fc5" />
                </button>
            </header>

            <h1>Registered books</h1>
            
            <ul>
                {books.map(book => {
                    return <li key={book.id}>
                        <strong>Title:</strong>
                        <p>{book.title}</p>
                        <strong>Author:</strong>
                        <p>{book.author}</p>
                        <strong>Price:</strong>
                        <p>{Intl.NumberFormat('pt-br', {style: 'currency', currency: 'BRL'}).format(book.price)}</p>
                        <strong>Release Date:</strong>
                        <p>{Intl.DateTimeFormat('pt-br').format(new Date(book.launch_Date))}</p>
                        <button type="button" onClick={() => editBook(book.id)}>
                            <FiEdit size={20} color="#251fc5"></FiEdit>
                        </button>
                        <button type="button" onClick={() => deleteBook(book.id)}>
                            <FiTrash2 size={20} color="#251fc5"></FiTrash2>
                        </button>
                    </li>
                })}
            </ul>

            <button type="button" className="button" onClick={fetchMoreBooks}>
                Load More
            </button>

        </div>
    );
}