import React, { Component } from 'react';
import { Route } from 'react-router';
import Layout from './Components/Layout';
import Home from './Pages/Home';
import Generate from './Pages/Generate';
import Upload from './Pages/Upload';


const App = () => {

    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/upload' component={Upload} />
        <Route  exact path='/generate' component={Generate} />
      </Layout>
    );
  
}

export default App;


