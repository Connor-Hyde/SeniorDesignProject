#include <ArduinoJson.h>
#include <SD.h> // include the SD library

// Defines pins numbers
const int stepPin = 12;
const int dirPin = 13; 

int customDelay,customDelayMapped; // Defines variables
 
void setup() {
  // Sets the two pins as Outputs
  pinMode(stepPin,OUTPUT); 
  pinMode(dirPin,OUTPUT);
  pinMode(10,OUTPUT); 
  pinMode(9,OUTPUT);
  Serial.begin(9600);
}
void loop() {
    // while (!Serial.available()){
    //       Serial.print("Waiting");
    //       delay(1000);

    // } // wait for data to be available

//   StaticJsonDocument<1024> doc; // adjust the size to match the serialized data
//   DeserializationError error = deserializeJson(doc, Serial);
//   DeserializationError error = deserializeJson(doc, "[[1,2,3,4,5,6,7,8,9,10],[2,4,6,8,10,12,14,16,18,20],[3,6,9,12,15,18,21,24,27,30],[4,8,12,16,20,24,28,32,36,40],[5,10,15,20,25,30,35,40,45,50],[6,12,18,24,30,36,42,48,54,60],[7,14,21,28,35,42,49,56,63,70],[8,16,24,32,40,48,56,64,72,80],[9,18,27,36,45,54,63,72,81,90],[10,20,30,40,50,60,70,80,90,100]]");


//   if (error) {
//     Serial.print("Error parsing JSON: ");
//     Serial.println(error.c_str());
//     return;
//   }
//     int grid[10][10];

// for (int row = 0; row < 10; row++) {
//     for (int col = 0; col < 10; col++) {
//       grid[row][col] = doc[row][col];
//       String rowString = String(row);
//       String colString = String(col);
//       //Serial.print("Row " + rowString + " Column " + colString + " Value: ");
//       Serial.print("Row: ");
//       Serial.print(row);
//       Serial.print(" Column: ");
//       Serial.print(col);
//       Serial.print(" Value: ");
//       Serial.print(doc[row][col].as<int>());
//       Serial.print("\n");
//       delay(1000);
//     }
// }

///Calibration Code

///Run motors row by row

///Wait for clear 

//EVERY ROTATION IS 1.25 millimeters

  digitalWrite(dirPin,HIGH); // Enables the motor to move in a particular direction
  digitalWrite(10,HIGH);
  digitalWrite(9,HIGH);
  // Makes 200 pulses for making one full cycle rotation
  for(int x = 0; x < 1425; x++) {
    digitalWrite(stepPin,HIGH); 
    digitalWrite(10,HIGH);
    digitalWrite(9,HIGH);
    delayMicroseconds(1500); 
    digitalWrite(stepPin,LOW); 
    digitalWrite(10,LOW);
    digitalWrite(9,LOW);
    delayMicroseconds(1500); 
  }
  delay(500); // One second delay
  digitalWrite(dirPin,LOW); //Changes the rotations direction
  digitalWrite(10,LOW);
  digitalWrite(9,LOW);
  // Makes 400 pulses for making two full cycle rotation
  for(int x = 0; x < 1425; x++) {
    digitalWrite(stepPin,HIGH);
    digitalWrite(10,HIGH);
    digitalWrite(9,HIGH);
    delayMicroseconds(1500);
    digitalWrite(stepPin,LOW);
    digitalWrite(10,LOW);
    digitalWrite(9,LOW);
    delayMicroseconds(1500);
  }
  delay(500);
}
