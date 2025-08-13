
CREATE TABLE  authors (
    id SERIAL PRIMARY KEY,
    fullname VARCHAR(255) NOT NULL,
    birthyear INT,
    country VARCHAR(100),
    createdat TIMESTAMP NOT NULL DEFAULT NOW(),
    updatedat TIMESTAMP NOT NULL DEFAULT NOW()
);


CREATE TABLE books (
    id SERIAL PRIMARY KEY,
    title VARCHAR(500) NOT NULL,
    authorid INT REFERENCES authors(id) ON DELETE CASCADE,
    genre VARCHAR(100),
    publishedyear INT,
    price NUMERIC(10,2),
    quantity INT NOT NULL DEFAULT 0,
    createdat TIMESTAMP NOT NULL DEFAULT NOW(),
    updatedat TIMESTAMP NOT NULL DEFAULT NOW()
);

CREATE TABLE  members (
    id SERIAL PRIMARY KEY,
    fullname VARCHAR(255) NOT NULL,
    phone VARCHAR(20),
    email VARCHAR(255) UNIQUE,
    membershipdate DATE NOT NULL,
    createdat TIMESTAMP NOT NULL DEFAULT NOW(),
    updatedat TIMESTAMP NOT NULL DEFAULT NOW()
);


CREATE TABLE  loans (
    id SERIAL PRIMARY KEY,
    bookid INT REFERENCES books(id) ON DELETE CASCADE,
    memberid INT REFERENCES members(id) ON DELETE CASCADE,
    loandate DATE NOT NULL,
    returndate DATE,
    isreturned BOOLEAN NOT NULL DEFAULT FALSE,
    createdat TIMESTAMP NOT NULL DEFAULT NOW(),
    updatedat TIMESTAMP NOT NULL DEFAULT NOW()
);



