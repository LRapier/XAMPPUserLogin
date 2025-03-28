<?php

require 'ConnectionSettings.php';

//user variables
$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Check connection
if ($conn->connect_error) 
{
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT password, id FROM user WHERE username = '" . $loginUser . "'";

$result = $conn->query($sql);

if ($result->num_rows > 0) 
{
    // Output data of each row
    while ($row = $result->fetch_assoc()) {
        if ($row["password"] == $loginPass) {
            echo $row["id"];
        } else {
            echo "Wrong Credentials.";
        }
    }
} 
else 
{
    echo "Username does not exist.";
}

$conn->close();
?>
