CREATE TABLE generator_table
(
    class_name varchar(256) CONSTRAINT PK_generator_table PRIMARY KEY
);

INSERT INTO generator_table(class_name)
VALUES ('Class1'),
       ('Class2'),
       ('Class3');