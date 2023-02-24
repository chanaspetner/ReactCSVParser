import axios from 'axios';
import React, { useEffect, useState } from 'react';

const Home = () => {
    const [people, setPeople] = useState([]);
    
    const getPeople = async() => {
        const { data } = await axios.get('/api/csv/getpeople');
        setPeople(data);
    }

    useEffect(() => {
        getPeople();
    }, []);

    const onDeleteClick = async() => {
        await axios.post('/api/csv/deleteall');
        getPeople();
    }

    return <>
        <div className="row">
            <div className="col-md-6 offset-md-3 mt-5">
                <button onClick={onDeleteClick} className="btn btn-danger btn-lg btn-block">Delete All</button>
            </div>
        </div>
        <table className='table table-hover table-striped table-bordered mt-5'>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Age</th>
                    <th>Address</th>
                    <th>Email</th>
                </tr>
            </thead>
            <tbody>
                {people.map((person, idx) => {
                    return <tr key={idx}>
                        <td>{person.id}</td>
                        <td>{person.firstName}</td>
                        <td>{person.lastName}</td>
                        <td>{person.age}</td>
                        <td>{person.address}</td>
                        <td>{person.email}</td>
                    </tr>
                })}
            </tbody>
        </table>
    </>
}

export default Home; 