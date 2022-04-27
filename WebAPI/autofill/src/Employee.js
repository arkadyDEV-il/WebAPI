import React from 'react';
import './AutoCompleteText.css' 
import {Image } from 'react-bootstrap';

export default class Employee extends React.Component{
    constructor(props){
        super(props);
        this.emp = this.props;
    }



render(){
    return(<div>
        <table>
            <tbody>
                <tr>
                    <td>Id:</td>
                    <td>{this.props.employee.EmployeeId}</td>
                </tr>
                <tr>
                    <td>Name:</td>
                    <td>{this.props.employee.EmployeeName}</td>
                </tr>
                <tr>
                    <td>Position:</td>
                    <td>{this.props.employee.Position}</td>
                </tr>
            </tbody>
        </table>
       Picture: <Image src={'http://localhost:5000/api/Employee/GetPicture/'+this.props.employee.EmployeeId}/>
    </div>)
}
}