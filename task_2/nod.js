const fs = require("fs");
sha3_256 = require("js-sha3").sha3_256;

let directory = process.argv[2];

fs.readdir(directory, (err, files) => {
  files.forEach((file) => {
    let hashed = sha3_256(fs.readFileSync(`${directory}/${file}`, "utf-8"));

    console.log(file, hashed);
  });
});
