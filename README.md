//installation steps 
1. chmod +x requirements.sh
2. ./ requirements.sh



//to read multiple groups 

method : get

url : http://0.0.0.0:84/read/multiplegroups

body input: {
                "ipaddress": "192.168.0.211",
                "port":102,
                "devicename":"PQMLD0",
                "varaddress":["M03_MMXU1$MX$PhV$phsA","M03_MMXU1$MX$TotW","M03_MMXU1$MX$TotVAr","M03_MMXU1$MX$TotVA","M03_MMXU1$MX$TotPF"]
}

//to read single group 

method : get

url : http://0.0.0.0:84/read/singlegroup 

body input: {
                "ipaddress": "192.168.0.118",
                "port":102,
                "fc":"ST",
                "varaddress":"AVR1TC230/GGIO1.GPO1"
            }


//to read only single variable

method : get

url : http://0.0.0.0:84/read/singlevariable

body input: {
                "ipaddress": "192.168.0.118",
                "port":102,
                "fc":"ST",
                "datatype" :"BOOLEAN",
                "varaddress":"AVR1TC230/GGIO1.GPO1.stVal"
            }

//to operate commands write operation

method : post

url : http://0.0.0.0:84/write/cmd

body input: {
                "ipaddress": "192.168.0.121",
                "port":102,
                "operate":false,
                "varaddress":"AA1E1Q01KF1CTRL/CBCSWI1.Pos"
            }


//to write value 

method : post

url : http://0.0.0.0:84/write/value

body input: {
                "ipaddress": "192.168.0.121",
                "port":102,
                "varaddress":"WAGOTestData/GGIO1.AnIn.subMag.f",
                "fc":"SV",
                "newvalue":55.765
            }


//to read dataset values

method : get

url : http://0.0.0.0:84/read/dataset

body input: {
                "ipaddress": "192.168.0.121",
                "port":102,
                "varaddress":"WAGOTestData/LLN0.Test"
            }

// to read multiple variable values

url : http://0.0.0.0:84/read/multiplevariable
method : get

body input: {
                "ipaddress": "192.168.0.72",
                "port":102,
                "varaddress":[
                                "WAGOF1/XGGIO1.Ind1.stVal[ST]",
                                "WAGOF1/XGGIO1.Ind1.t[ST]",
                                "WAGOF1/XGGIO1.Ind1.q[ST]",
                                "WAGOF2/XGGIO1.Ind1.stVal[ST]",
                                "WAGOF2/XGGIO1.Ind1.t[ST]",
                                "WAGOF2/XGGIO1.Ind1.q[ST]"
                            ]
            }
