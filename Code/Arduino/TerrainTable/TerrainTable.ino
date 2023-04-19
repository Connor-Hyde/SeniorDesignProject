#include <ArduinoJson.h>

// Defines pins numbers
const int stepPin1 = 7;
const int stepPin2 = 8;
const int stepPin3 = 6;
const int stepPin4 = 9;
const int stepPin5 = 5;
const int stepPin6 = 10;
const int stepPin7 = 4;
const int stepPin8 = 11;
const int stepPin9 = 3;
const int stepPin10 = 12;
const int stepPinMasterSlave = 2;
const int stepPinRow = 12;

const int dirPin10 = 13; 
const int dirPin2 = 13; 
const int dirPin = 13; 


void setup() {
  // put your setup code here, to run once:
  pinMode(dirPin10,OUTPUT);
  pinMode(dirPin2,OUTPUT);
  pinMode(dirPin,OUTPUT);

  pinMode(stepPinRow,OUTPUT);
  pinMode(stepPinMasterSlave,OUTPUT);
  pinMode(stepPin1,OUTPUT);
  pinMode(stepPin2,OUTPUT);
  pinMode(stepPin3,OUTPUT);
  pinMode(stepPin4,OUTPUT);
  pinMode(stepPin5,OUTPUT);
  pinMode(stepPin6,OUTPUT);
  pinMode(stepPin7,OUTPUT);
  pinMode(stepPin8,OUTPUT);
  pinMode(stepPin9,OUTPUT);
  pinMode(stepPin10,OUTPUT);

  Serial.begin(9600);
}

void loop() {
  //Bring motors to row one - we can handle calibration of cubes in a separate sketch
  //Read for when the switch closes, stop the motor controlling row swap - now at row 1
  // 1. Bring motors up into cubes (.78 inches up into cubes) - 2 motors doing this
  // 2. Perform row 1 cube movements - 10 motors 
  // 3. Bring motors down from cubes (.78 inches down)
  // 4. Move forward 1 row (1 inch) - 1 motor
  // repeat steps 1 - 4 for all rows

  //Variable pins for now
  //while (!Serial.available()){} // wait for data to be available

  StaticJsonDocument<1024> doc; // adjust the size to match the serialized data
  //DeserializationError error = deserializeJson(doc, Serial);
  DeserializationError error = deserializeJson(doc, "[[3.0,3.0,3.0,3.0,3.0,3.0,3.0,3.0,3.0,3.0],[0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0],[0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0],[0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0],[0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0],[0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0],[0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0],[0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0],[0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0],[0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0,0.0]]");

  int grid[10][10];

  for (int row = 0; row < 10; row++) {
    for (int col = 0; col < 10; col++) {
      grid[row][col] = doc[row][col];
    }
  }

  //Set dirPin to HIGH
  digitalWrite(dirPin10, LOW);
  for (int gridRow = 0; gridRow < 10; gridRow++){

    //Raise motors to meet cube bracket
    digitalWrite(dirPin2, HIGH);
    for(int x = 0; x < 2800; x++){
      digitalWrite(stepPinMasterSlave, HIGH);
      digitalWrite(stepPinMasterSlave, LOW);
      delayMicroseconds(1500);
    }

    //Motor1 height = grid[gridRow][0];
    float h1 = grid[gridRow][0];
    h1 = (h1 / 100) * 3;
    int pulses1 = (h1 / 0.0654527559) * 1425;
    //Motor2 height2 = grid[gridRow][1];
    float h2 = grid[gridRow][1];
    h2 = (h2 / 100) * 3;
    int pulses2 = (h2 / 0.0654527559) * 1425;
    //Motor3 height3 = grid[gridRow][2];
    float h3 = grid[gridRow][2];
    h3 = (h3 / 100) * 3;
    int pulses3 = (h3 / 0.0654527559) * 1425;
    //Motor4 height4 = grid[gridRow][3];
    float h4 = grid[gridRow][3];
    h4 = (h4 / 100) * 3;
    int pulses4 = (h4 / 0.0654527559) * 1425;
    //Motor5 height5 = grid[gridRow][4];
    float h5 = grid[gridRow][4];
    h5 = (h5 / 100) * 3;
    int pulses5 = (h5 / 0.0654527559) * 1425;
    //Motor6 height6 = grid[gridRow][5];
    float h6 = grid[gridRow][5];
    h6 = (h6 / 100) * 3;
    int pulses6 = (h6 / 0.0654527559) * 1425;
    //Motor7 height7 = grid[gridRow][6];
    float h7 = grid[gridRow][6];
    h7 = (h7 / 100) * 3;
    int pulses7 = (h7 / 0.0654527559) * 1425;
    //Motor8 height8 = grid[gridRow][7];
    float h8 = grid[gridRow][7];
    h8 = (h8 / 100) * 3;
    int pulses8 = (h8 / 0.0654527559) * 1425;
    //Motor9 height9 = grid[gridRow][8];
    float h9 = grid[gridRow][8];
    h9 = (h9 / 100) * 3;
    int pulses9 = (h9 / 0.0654527559) * 1425;
    //Motor10 height10 = grid[gridRow][9];
    float h10 = grid[gridRow][9];
    h10 = (h10 / 100) * 3;
    int pulses10 = (h10 / 0.0654527559) * 1425;

    //Just putting the max possible height here for now
    for(int x = 0; x < 65315; x++) {
        delayMicroseconds(300); 
      if(x < pulses1){
        digitalWrite(stepPin1, HIGH);
        delayMicroseconds(300); 
        digitalWrite(stepPin1,LOW); 
        //delayMicroseconds(900); 
      }
      else{ delayMicroseconds(300); }

      if(x < pulses2){
        digitalWrite(stepPin2, HIGH);
        delayMicroseconds(300); 
        digitalWrite(stepPin2,LOW); 
        //delayMicroseconds(900); 
      }
      else{ delayMicroseconds(300); }

      if(x < pulses3){
        digitalWrite(stepPin3, HIGH);
        delayMicroseconds(300); 
        digitalWrite(stepPin3,LOW); 
        //delayMicroseconds(900); 
      }
      else{ delayMicroseconds(300); }

      if(x < pulses4){
        digitalWrite(stepPin4, HIGH);
        delayMicroseconds(300); 
        digitalWrite(stepPin4,LOW); 
        //delayMicroseconds(900); 
      }
      else{ delayMicroseconds(300); }

      if(x < pulses5){
        digitalWrite(stepPin5, HIGH);
        delayMicroseconds(300); 
        digitalWrite(stepPin5,LOW); 
        //delayMicroseconds(900); 
      }
      else{ delayMicroseconds(300); }

      if(x < pulses6){
        digitalWrite(stepPin6, HIGH);
        delayMicroseconds(300); 
        digitalWrite(stepPin6,LOW); 
        //delayMicroseconds(900); 
      }
      else{ delayMicroseconds(300); }

      if(x < pulses7){
        digitalWrite(stepPin7, HIGH);
        delayMicroseconds(300); 
        digitalWrite(stepPin7,LOW); 
        //delayMicroseconds(900); 
      }
      else{ delayMicroseconds(300); }

      if(x < pulses8){
        digitalWrite(stepPin8, HIGH);
        delayMicroseconds(300); 
        digitalWrite(stepPin8,LOW); 
        //delayMicroseconds(900); 
      }
      else{ delayMicroseconds(300); }

      if(x < pulses9){
        digitalWrite(stepPin9, HIGH);
        delayMicroseconds(300); 
        digitalWrite(stepPin9,LOW); 
        //delayMicroseconds(900); 
      }
      else{ delayMicroseconds(300); }

      if(x < pulses10){
        digitalWrite(stepPin10, HIGH);
        delayMicroseconds(300); 
        digitalWrite(stepPin10,LOW); 
        //delayMicroseconds(900); 
      }
      else{ delayMicroseconds(300); }

    
    //Lower motors to remove from cube bracket
    digitalWrite(dirPin2, LOW);
    for(int x = 0; x < 2800; x++){
      digitalWrite(stepPinMasterSlave, HIGH);
      digitalWrite(stepPinMasterSlave, LOW);
      delayMicroseconds(1500);
    }

    //Move forward a row unless on last row
    digitalWrite(dirPin, LOW);
    for(int x = 0; x < 130; x++){
      digitalWrite(stepPinRow, HIGH);
      digitalWrite(stepPinRow, LOW);
      delay(15);
    }
  }
}

}
