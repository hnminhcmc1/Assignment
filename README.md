# Assignment
1. Choose Package-Manager Console

2. Run Update-Database at Assignment.UserData

3. For Test API (Use Postman)

- Register: [POST] http://localhost:56388/api/user/register

Example param : 
{
    "name": "MinhHoang25",
    "email": "MinhHoang25@gmail.com",
    "password": 1234,
    "mobileNumber": "12345",
    "gender": "Nam",
    "dob": "1995-12-25T00:00:00",
    "emailOptIn": "MinhHoang25@gmail.com"    
}

- Login: [POST] http://localhost:56388/api/user/login

Example param: {             
    "email": "MinhHoang25@gmail.com",
    "password": "1234"        
}

- Show profile: [GET] http://localhost:56388/api/user/{id}

Example: http://localhost:56388/api/user/1

- Update User: [PUT] http://localhost:56388/api/user/edit

Example param:
{
     "id": 1,
     "name": "MinhHoang65",
     "email": "MinhHoang65@gmail.com",
     "password": "1234",
     "mobileNumber": "12345",
     "gender": "Nam",
     "dob": "1995-12-25T00:00:00",
     "emailOptIn": "MinhHoang2@gmail.com",
}
