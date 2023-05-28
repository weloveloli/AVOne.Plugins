#!/usr/bin/env python3
import hashlib
import json
import sys
from datetime import datetime
from urllib.request import urlopen


def md5sum(filename):
    with open(filename, 'rb') as f:
        return hashlib.md5(f.read()).hexdigest()


def generate(filename, version):
    return {
        'checksum': md5sum(filename),
        'changelog': 'Auto Released by Actions',
        'targetAbi': '0.2.6.0',
        'sourceUrl': 'https://github.com/weloveloli/AVOne.Plugins/releases/download/'
                     f'v{version}/AVOne.Plugin.Extractors@v{version}.zip',
        'timestamp': datetime.now().strftime('%y-%m-%d %I:%M:%S'),
        'version': version
    }


def main():
    filename = sys.argv[1]
    version = filename.split('@', maxsplit=1)[1] \
        .removeprefix('v') \
        .removesuffix('.zip')

    with urlopen('https://raw.githubusercontent.com/weloveloli/AVOne.Plugins/dist/manifest.json') as f:
        manifest = json.load(f)

    manifest[2]['versions'].insert(0, generate(filename, version))
    manifest[2]['versions'] = sorted(manifest[2]['versions'], key=lambda x: x["version"], reverse=True)
    with open('manifest.json', 'w') as f:
        json.dump(manifest, f, indent=2)


if __name__ == '__main__':
    main()
