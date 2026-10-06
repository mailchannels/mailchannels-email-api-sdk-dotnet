import json
import sys
from pathlib import Path
report = json.loads(Path(sys.argv[1]).read_text())
assert report.get('projects'), 'Audit returned no projects'
def check(value):
    if isinstance(value, dict):
        assert not value.get('vulnerabilities'), 'Known vulnerabilities reported'
        assert value.get('level', '').lower() not in {'error', 'warning'}, 'Audit diagnostics need review'
        for child in value.values(): check(child)
    elif isinstance(value, list):
        for child in value: check(child)
check(report)
print('No known vulnerabilities or audit warnings reported')
