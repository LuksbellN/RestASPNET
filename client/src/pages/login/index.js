import React, {useState} from "react";
import { useNavigate } from 'react-router-dom';
import './styles.css';

import logoImage from '../../assets/logo.svg';
import padLock from '../../assets/padlock.png';
import api from '../../services/api'

export default function Login() {

    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');

    const navigate = useNavigate();

    async function login(e) {
        e.preventDefault();

        const data = {
            userName,
            password
        };

        try{
            const response = await api.post('v1/api/auth/signin', data);
            
            localStorage.setItem('userName', userName);
            localStorage.setItem('accessToken', response.data.accessToken);
            localStorage.setItem('refreshToken', response.data.refreshToken);

            navigate('/books');
        } catch(error){
            alert("Email and/or password Invalid!")
        }


    }

    return (
        <div>
            <div className="login-container">
            <section className="form">
                <img src={logoImage} alt="ErudioLogo" />

                <form onSubmit={login}>
                    <h1>Access your Account</h1>

                    <input
                         placeholder="Username" 
                         value={userName}
                         onChange={e => setUserName(e.target.value)}
                    />
                    <input
                         type="password" placeholder="Password" 
                         value={password}
                         onChange={e => setPassword(e.target.value)}
                    />

                    <button className="button" type="submit">Login</button>
                </form>

            </section>


            <img src={padLock} alt="Login" />
            </div>
        </div>
    );
}