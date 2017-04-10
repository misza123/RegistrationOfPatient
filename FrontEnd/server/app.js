const path = require('path');

const express = require('express');
const connectLivereload = require('connect-livereload');

const app = express();

app.use(connectLivereload());
app.use('/public', express.static(path.join(__dirname, '../.tmp/public')));

app.get('/', (req, res) => {
    res.sendFile('.tmp/index.html', {root: path.join(__dirname, '..')});
});

app.listen(3000, () => {
    process.stdout.write('Development server listening on port 3000\r\n');
});

module.exports.app = exports.app = app;
