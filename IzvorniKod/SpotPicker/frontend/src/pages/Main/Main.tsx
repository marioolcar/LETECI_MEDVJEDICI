import { Button } from '@mui/material';
import React from 'react';
import { useNavigate } from 'react-router-dom';

export function Main(){
    const navigate = useNavigate();
    const handleClick = () => {
        localStorage.removeItem('jwt-token');
    }
    return (
        <>
                <h1>You are logged in</h1>
                <Button variant='contained' onClick={handleClick}>Log out</Button>
        </>
    )
}