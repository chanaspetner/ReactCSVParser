import React, { useRef, useState } from 'react';
import axios from 'axios';

const Generate = () => {

    const [amount, setAmount] = useState('');


    const onGenerateClick = () => {
       window.location.href = `/download/generatecsv?stringAmount=${amount}`;
    }

    return <div className="d-flex w-100 justify-content-center align-self-center">
        <div className="row">
            <input type="text"  value={amount} onChange={e => setAmount(e.target.value)} className="form-control-lg" placeholder="Amount" />            
        </div>
        <div className="row">
            <div className="col-md-4">
                <button className="btn btn-primary btn-lg" onClick={onGenerateClick}>Generate</button>
            </div>
        </div>
    </div>
}

export default Generate;