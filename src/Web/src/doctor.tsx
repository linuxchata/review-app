import * as React from 'react';

import './styles/doctor.scss';
import * as doctor from './images/sample_doctor.png';

class Doctor extends React.Component<{}, {}>{
  render() {
    return (
      <div>
        <section className='doctor-main'>
          <div className='clearfix' />
          <div className='left-side'>
            <img src={doctor} alt='photo' />
          </div>
          <div className='right-side'>
            <p className='name'>mgr Adam Kondrad Lewanowicz</p>
            <p className='spec'>Psycholog, Terapeuta, Psychoterapeuta</p>
            <div className='rating-wrapper'>
              <div className='rating'>
                <span>☆</span>
                <span>☆</span>
                <span>★</span>
                <span>★</span>
                <span>★</span>
              </div>
            </div>
            <p className='desc'>Nie jestem idealny, jestem wystarczająco dobry :)) Serdecznie zapraszam :) Podstawą dla mnie jest relacja z klientem.
              Jeżeli nie ma wolnego terminu proszę o kontakt :) Przy…
            </p>
          </div>
        </section>

        <section className='doctor-additional'>
          <header>
            <h2>Qualification</h2>
          </header>
          <div className='left-side'>
            <div className='content'>
              <h3>About</h3>
              <p>
                Nie jestem idealny, jestem wystarczająco dobry :)) Serdecznie zapraszam :) Podstawą dla mnie jest relacja z klientem. Jeżeli
                nie ma wolnego terminu proszę o kontakt :) Przy zapisywaniu , o ile jest taka możliwość proszę o nie robienie 'okienek'
                :)
              </p>
            </div>
            <div className='content'>
              <h3>Specjalizacje</h3>
              <p>
                Nie jestem idealny, jestem wystarczająco dobry :)) Serdecznie zapraszam :) Podstawą dla mnie jest relacja z klientem. Jeżeli
                nie ma wolnego terminu proszę o kontakt :) Przy zapisywaniu , o ile jest taka możliwość proszę o nie robienie 'okienek'
                :)
            </p>
            </div>
          </div>
          <div className='right-side'>
            <div className='content'>
              <h3>Znajomość języków</h3>
              <p>
                Nie jestem idealny, jestem wystarczająco dobry :)) Serdecznie zapraszam :) Podstawą dla mnie jest relacja z klientem. Jeżeli
                nie ma wolnego terminu proszę o kontakt :) Przy zapisywaniu , o ile jest taka możliwość proszę o nie robienie 'okienek'
                :) Nie jestem idealny, jestem wystarczająco dobry :)) Serdecznie zapraszam :) Podstawą dla mnie jest relacja z
                klientem. Jeżeli nie ma wolnego terminu proszę o kontakt :) Przy zapisywaniu , o ile jest taka możliwość proszę
                o nie robienie 'okienek' :)
              </p>
            </div>
            <div className='content'>
              <h3>Staże</h3>
              <p>
                Nie jestem idealny, jestem wystarczająco dobry :)) Serdecznie zapraszam :) Podstawą dla mnie jest relacja z klientem. Jeżeli
                nie ma wolnego terminu proszę o kontakt :) Przy zapisywaniu , o ile jest taka możliwość proszę o nie robienie 'okienek'
                :)
              </p>
            </div>
            <div className='content'>
              <h3>Certyfikaty</h3>
              <p>
                Nie jestem idealny, jestem wystarczająco dobry :)) Serdecznie zapraszam :) Podstawą dla mnie jest relacja z klientem. Jeżeli
                nie ma wolnego terminu proszę o kontakt :) Przy zapisywaniu , o ile jest taka możliwość proszę o nie robienie 'okienek'
                :)
              </p>
            </div>
          </div>
        </section>
      </div>
    )
  }
}

export default Doctor;