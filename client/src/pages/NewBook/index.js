import React, { useEffect, useState } from "react";
import "./styles.css";
import logoImage from '../../assets/logo.svg';
import { Link, useNavigate, useParams } from "react-router-dom";
import { FiArrowLeft } from "react-icons/fi";
import api from "../../services/api";


export default function NewBook(){

    const [id, setId] = useState(null);
    const [title, setTitle] = useState('');
    const [author, setAuthor] = useState('');
    const [launch_date, setLaunch_date] = useState('');
    const [price, setPrice] = useState();

    const { bookId } = useParams(); 

    const navigate = useNavigate();

    const Authorization = "Bearer "+localStorage.accessToken;

    const config = {
        headers:{
            Authorization
        }
    };

    async function saveOrUpdate(e){
        e.preventDefault();

        const book = {
            title,
            author,
            launch_date,
            price
        };

        try{
            if(bookId === '0'){
                await api.post('v1/api/book', book, config);
            } else {
                book.id = id;
                await api.put('v1/api/book', book, config);
            }
            
            navigate('/books')
        } catch(error){
          alert('Error while recording book!');  
        }

    }
    
    useEffect(() => {
        if(bookId !== '0') {
            const loadBook = async () => {
                try{
                    await api.get(`v1/api/book/${bookId}`, config)
                            .then(response => {
                                setId(response.data.id);
                                setTitle(response.data.title);
                                setAuthor(response.data.author);
                                let date = response.data.launch_Date.split('').slice(0,10).join('');
                                setLaunch_date(date);
                                setPrice(response.data.price);
                            });
                } catch (error) {
                    alert(`Get book id ${bookId} failed!`);
                    navigate('/books');
                }
            }
            loadBook();
        }
    }, [bookId]);


    return (
        <div className="new-book-container">
            <div className="content">
                <section className="form">
                    <img src={logoImage} alt="Erudio"/>
                    <h1>{bookId === '0' ? 'Add new' : 'Update'} Book</h1>
                    <p>Enter the book information and click on '{bookId === '0' ? 'Add' : 'Update'}'!</p>
                    <Link className="back-link" to="/books">
                        <FiArrowLeft size={16} color="#251fc5"></FiArrowLeft>
                        Home
                    </Link>
                </section>
                <form onSubmit={saveOrUpdate}>
                    <input
                         placeholder="Title"
                         value={title}
                         onChange={e => setTitle(e.target.value)}
                    />
                    <input
                        placeholder="Author"
                        value={author}
                        onChange={e => setAuthor(e.target.value)}
                    />
                    <input 
                        type="Date"
                        value={launch_date}
                        onChange={e => setLaunch_date(e.target.value)}    
                    />
                    <input
                        placeholder="Price"
                        value={price}
                        onChange={e => setPrice(e.target.value)}
                    />

                    <button className="button" type="submit">{bookId === '0' ? 'Add' : 'Update'}</button>
                </form>
            </div>
        </div>
    );
}