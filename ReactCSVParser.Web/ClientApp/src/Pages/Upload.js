import React, { useRef } from 'react';
import axios from 'axios';
import { useHistory } from 'react-router';

const Upload = () => {

    const history = useHistory();

    const toBase64 = file => new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => resolve(reader.result);
        reader.onerror = error => reject(error);

    });

    const fileInputRef = useRef(null);

    const onUploadClick = async() => {
        const file = fileInputRef.current.files[0];
        const base64 = await toBase64(file);
        const name = file.name;
        await axios.post('api/csv/upload', { base64Csv: base64, name});
        history.push('/');
    }


    return<div className="d-flex w-100 justify-content-center align-self-center">
        <div className="row">
            <div className="col-md-10">
                <input ref={fileInputRef} type="file" className="form-control-lg" />
            </div>
            <div className="col-md-2">
                <button onClick={onUploadClick} className="btn btn-primary btn-lg">Upload</button>
            </div>
        </div>
    </div>
}

export default Upload;