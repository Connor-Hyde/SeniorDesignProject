#include <ArduinoJson.h>


const int stepPin = 12;
const int dirPin = 13;
const int switchPin = 7;


void setup() {
  pinMode(stepPin,OUTPUT); 
  pinMode(dirPin,OUTPUT);
  pinMode(switchPin,OUTPUT); 
  Serial.begin(9600);
  digitalWrite(switchPin,HIGH);
  Serial.println(digitalRead(switchPin)); 
}

void loop() {

while (digitalRead(switchPin) != LOW) {

}

  //Rotate cubes
  digitalWrite(dirPin, LOW);
  for(int x = 0; x < 14250; x++){
    digitalWrite(stepPin, HIGH);
    digitalWrite(stepPin, LOW);
    delayMicroseconds(1500);
  }
  while (digitalRead(switchPin) != LOW) {

  }

  //Lower
  for(int x = 0; x < 2800; x++){
    digitalWrite(stepPin, HIGH);
    digitalWrite(stepPin, LOW);
    delayMicroseconds(1500);
  }
  while (digitalRead(switchPin) != LOW) {

  }

  //Move forward
  for(int x = 0; x < 130; x++){
    digitalWrite(stepPin, HIGH);
    digitalWrite(stepPin, LOW);
    delay(15);
  }
  while (digitalRead(switchPin) != LOW) {

  }

  //Raise
  digitalWrite(dirPin, HIGH);
  for(int x = 0; x < 2800; x++){
    digitalWrite(stepPin, HIGH);
    digitalWrite(stepPin, LOW);
    delayMicroseconds(1500);
  }
  while (digitalRead(switchPin) != LOW) {

  }

  //Rotate cubes
  digitalWrite(dirPin, LOW);
  for(int x = 0; x < 14250; x++){
    digitalWrite(stepPin, HIGH);
    digitalWrite(stepPin, LOW);
    delayMicroseconds(1500);
  }
  while (digitalRead(switchPin) != LOW) {

  }

  //Rotate cubes
  digitalWrite(dirPin, HIGH);
  for(int x = 0; x < 14250; x++){
    digitalWrite(stepPin, HIGH);
    digitalWrite(stepPin, LOW);
    delayMicroseconds(1500);
  }
  while (digitalRead(switchPin) != LOW) {

  }

  digitalWrite(dirPin, LOW);
  //Lower
  for(int x = 0; x < 2800; x++){
    digitalWrite(stepPin, HIGH);
    digitalWrite(stepPin, LOW);
    delayMicroseconds(1500);
  }
  while (digitalRead(switchPin) != LOW) {

  }

  //Bring back to beginning
  digitalWrite(dirPin, HIGH);
  for(int x = 0; x < 130; x++){
    digitalWrite(stepPin, HIGH);
    digitalWrite(stepPin, LOW);
    delay(15);
  }
  while (digitalRead(switchPin) != LOW) {

  }
  //Raise
  for(int x = 0; x < 2800; x++){
    digitalWrite(stepPin, HIGH);
    digitalWrite(stepPin, LOW);
    delayMicroseconds(1500);
  }
  while (digitalRead(switchPin) != LOW) {

  }

  //Rotate cubes
  digitalWrite(dirPin, HIGH);
  for(int x = 0; x < 14250; x++){
    digitalWrite(stepPin, HIGH);
    digitalWrite(stepPin, LOW);
    delayMicroseconds(1500);
  }
  while (digitalRead(switchPin) != LOW) {

  }
  
}
