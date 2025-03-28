<?php

require 'ConnectionSettings.php';

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
echo "Connected successfully, now we will show the users.<br><br>";

$sql = "SELECT username, level FROM user";

$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // Output data of each row
    while ($row = $result->fetch_assoc()) {
        echo "Username: " . $row["username"] . " - Level: " . $row["level"] . "<br>";
    }
} else {
    echo "0 results";
}

$conn->close();
?>
