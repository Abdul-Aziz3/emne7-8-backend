# create user
CREATE USER IF NOT EXISTS 'ga-app'@'studentblogg-db' IDENTIFIED BY 'ga-5ecret-%';
CREATE USER IF NOT EXISTS 'ga-app'@'%' IDENTIFIED BY 'ga-5ecret-%';
 
GRANT ALL privileges ON ga_emne7_studentblogg.* TO 'ga-app'@'%';
GRANT ALL privileges ON ga_emne7_studentblogg.* TO 'ga-app'@'studentblogg-db';
 
FLUSH PRIVILEGES;
